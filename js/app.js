var foodApi = "http://localhost:54634/api/Food";
var getFoodApi = "http://localhost:54634/api/BookFood/GetBooksFood";
var bookFoodApi = "http://localhost:54634/api/BookFood";
var userApi = "http://localhost:54634/api/Auth/Id?Id=";
var container = document.getElementById("app-container");
var exploreContainer = document.getElementById("explore-container");

function start() {
  getStore(renderStore);
  getUser(1);
}

start();

//Get store
function getStore(callback) {
  fetch(foodApi)
    .then((response) => response.json())
    .then(callback);
}

//Get user
function getUser(id) {
  fetch(userApi + id)
    .then((response) => response.json())
    .then((user) => {
      // console.log(user);
    });
}

//Create Store
function createStore(data) {
  var options = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  };
  fetch(bookFoodApi, options)
    .then((response) => response.json())
    .then();
}

//Render store
function renderStore(stores) {
  var container = document.getElementById("container-store-data");
  var htmls = stores.data.map((store) => {
    return `
      <div class="col-12 col-lg-3 mt-3">
        <div class="card" style="width: 100%">
          <img
            src="${store.imageLink}"
            class="card-img-top"
            alt="..."
          />
          <div class="card-body">
            <h5 class="card-title">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="16"
                height="16"
                fill="currentColor"
                class="bi bi-bookmark-fill mb-1"
                viewBox="0 0 16 16"
              >
                <path
                  d="M2 2v13.5a.5.5 0 0 0 .74.439L8 13.069l5.26 2.87A.5.5 0 0 0 14 15.5V2a2 2 0 0 0-2-2H4a2 2 0 0 0-2 2z"
                />
              </svg>
              ${store.foodName}
            </h5>
            <p class="cart-text">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="16"
                height="16"
                fill="currentColor"
                class="bi bi-tags-fill mb-1"
                viewBox="0 0 16 16"
              >
                <path
                  d="M2 2a1 1 0 0 1 1-1h4.586a1 1 0 0 1 .707.293l7 7a1 1 0 0 1 0 1.414l-4.586 4.586a1 1 0 0 1-1.414 0l-7-7A1 1 0 0 1 2 6.586V2zm3.5 4a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3z"
                />
                <path
                  d="M1.293 7.793A1 1 0 0 1 1 7.086V2a1 1 0 0 0-1 1v4.586a1 1 0 0 0 .293.707l7 7a1 1 0 0 0 1.414 0l.043-.043-7.457-7.457z"
                />
              </svg>
              Giá: ${store.foodPrice}đ
            </p>
            <button class="btn btn-primary" onclick="clickExplore('${encodeURIComponent(
              JSON.stringify(store)
            )}')">Đặt món</button>
          </div>
        </div>
      </div>
        `;
  });
  container.innerHTML = htmls.join("");
}

//Check login
function checkLogin() {}

function clickExplore(store) {
  if (localStorage.getItem("username") === null) {
    location.replace("http://127.0.0.1:5500/login.html");
  } else {
    store = JSON.parse(decodeURIComponent(store));
    console.log(store.id);
    container.classList.add("d-none");
    exploreContainer.classList.remove("d-none");
    var htmls = `
  <div class="container bg-light shadow mt-3">
        <div class="row">
          <div class="col-lg-4 text-center">
            <img
              src="${store.imageLink}"
              class="img-fluid mt-1 mt-lg-5 rounded-3 shadow mb-lg-5"
              alt="..."
            />
          </div>
          <div class="col-lg-8">
            <div class="row">
              <h1 class="text-center mt-3 mt-lg-1">${store.foodName}</h1>

              <div class="info-container d-flex flex-column justify-content-center">

                <div class="d-flex mt-4">
                  <h2>Giá:</h2>
                  <h2 class="text-danger ms-2">${store.foodPrice}đ</h2>
                </div>
                <h3 class="mt-4">
                  <label for="quantity">Số lượng:</label>
                  <input type="number" id="quantity" name="quantity" min="0">
                </h3>
                <h3 class="mt-4" id="total">
                  Tổng tiền: 
                </h3>
                <div class="d-flex justify-content-center align-items-center mb-3">
                  <button class="btn btn-primary mt-5 mb-2 mb-lg-0 me-5" id="calculate-btn">
                    <h3>Tính tiền</h3>
                  </button>
                  <button class="btn btn-primary mt-5 mb-2 mb-lg-0 ms-5" id="add-to-cart">
                    <h3>Thêm vào giỏ hàng</h3>
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
  `;
    exploreContainer.innerHTML = htmls;
    var addToCartBtn = document.getElementById("add-to-cart");
    var calculateBtn = document.getElementById("calculate-btn");
    calculateBtn.onclick = () => {
      var quantity = document.querySelector('input[name="quantity"]').value;
      var total = document.getElementById("total");
      total.value = "Tổng tiền: " + store.foodPrice * quantity + "đ";
      total.innerHTML = total.value;
    };
    addToCartBtn.onclick = () => {
      container.classList.remove("d-none");
      exploreContainer.classList.add("d-none");
      var quantity = document.querySelector('input[name="quantity"]').value;
      var total = store.foodPrice * quantity;

      if (localStorage.getItem('description') != null) {
        localStorage.setItem('description', localStorage.getItem('description') + " " + store.foodName + " x " + quantity + ". ")
      } else {
        localStorage.setItem('description', store.foodName + " x " + quantity + ". ")
      }

      var form = {
        foodId: store.id,
        quantity: parseInt(quantity),
        userId: parseInt(localStorage.getItem('userId')),
        total: total
      };

      createStore(form);
    };
  }
}
