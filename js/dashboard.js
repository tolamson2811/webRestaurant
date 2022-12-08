//Khai bao
var foodApi = "http://localhost:54634/api/Food";
var wrapper = document.getElementById("dashboard-wrapper");
var addContainer = document.getElementById("add-container");
var container = document.getElementById("dashboard-info");
var saveAddButton = document.querySelector(".save-add-button");
var editContainer = document.getElementById("edit-container");
var overlay = document.getElementById("overlay");
var overlay1 = document.getElementById("overlay1");

//Start function
function start() {
  getStore(renderStore);
  handleSaveAdd();
}

start();

//Get store
function getStore(callback) {
  fetch(foodApi)
    .then((response) => response.json())
    .then(callback);
}

//Delete store
function deleteStore(id) {
  var data = {
    id: id,
  };
  var options = {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  };
  fetch(foodApi, options)
    .then((response) => response.json())
    .then();
  location.reload();
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
  fetch(foodApi, options)
    .then((response) => response.json())
    .then(() => {
      overlay.classList.remove("d-none");
      setTimeout(() => {
        overlay.classList.add("d-none");
      }, 2000);
    });
}

//Edit store
function modifyStore(data) {
  var options = {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  };
  fetch(foodApi, options)
    .then((response) => response.json())
    .then(() => {
      overlay.classList.remove("d-none");
      setTimeout(() => {
        overlay.classList.add("d-none");
      }, 2000);
    });
}

//Render store len man hinh
function renderStore(stores) {
  var htmls = stores.data.map((store) => {
    return `
    <div class="row container-data">
          <hr />
          <div class="col-2 d-flex align-items-center justify-content-center">
            <p>${store.foodName}</p>
          </div>
          <div class="col-2 d-flex align-items-center justify-content-center">
            <p>${store.foodPrice}đ</p>
          </div>
          <div class="col-4 d-flex align-items-center justify-content-center">
            <img
              src="${store.imageLink}"
              class="img-fluid mb-3 rounded"
              alt="..."
            />
          </div>
          <div class="col-2 d-flex align-items-center mb-3 justify-content-center">
              <button class="btn btn-primary" onclick="clickEdit(${store.id})">Sửa</button>
          </div>
          <div class="col-2 d-flex align-items-center mb-3 justify-content-center">
            <button class="btn btn-primary" onclick="deleteStore(${store.id})">Xóa</button>
          </div>
        </div>
    `;
  });
  container.innerHTML = htmls.join("");
}

//Ham xu li save khi add
function handleSaveAdd() {
  saveAddButton.onclick = () => {
    var name = document.querySelector('input[name="name"]').value;
    var price = document.querySelector('input[name="price"]').value;
    var image = document.querySelector('input[name="image"]').value;

    var form = {
      foodName: name,
      foodPrice: parseInt(price),
      imageLink: image,
    };
    document.querySelector('input[name="name"]').value = "";
    document.querySelector('input[name="price"]').value = "";
    var image = (document.querySelector('input[name="image"]').value = "");
    createStore(form);
  };
}

//Xu li su kien khi an vao cac button
var addButton = document.getElementById("add-store-button");
addButton.onclick = () => {
  wrapper.classList.add("d-none");
  addContainer.classList.remove("d-none");
};

var backAddButton = document.getElementById("back-add-button");
backAddButton.onclick = () => {
  wrapper.classList.remove("d-none");
  addContainer.classList.add("d-none");
};

//Edit
function clickEdit(id) {
  wrapper.classList.add("d-none");
  editContainer.classList.remove("d-none");
  var saveEditButton = document.getElementById("save-edit-button");
  saveEditButton.onclick = () => {
    var name = document.querySelector('input[name="nameEdit"]').value;
    var image = document.querySelector('input[name="imageEdit"]').value;
    var price = document.querySelector('input[name="priceEdit"]').value;

    var form = {
      id: id,
      foodName: name,
      imageLink: image,
      foodPrice: parseInt(price),
    };
    document.querySelector('input[name="nameEdit"]').value = "";
    document.querySelector('input[name="imageEdit"]').value = "";
    document.querySelector('input[name="priceEdit"]').value = "";
    modifyStore(form);
  };
}

var backEditButton = document.getElementById("back-edit-button");
backEditButton.onclick = () => {
  wrapper.classList.remove("d-none");
  editContainer.classList.add("d-none");
};
