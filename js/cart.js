var ordersApi = "http://localhost:54634/api/BookFood";
var foodIdApi = "http://localhost:54634/api/Food/Id?Id=";
var bookFoodApi = "http://localhost:54634/api/BookFood";
var getUserApi = "http://localhost:54634/api/Auth/Id?Id=";
var purchaseApi = 'http://localhost:54634/api/Purchase'
var payButton = document.getElementById("pay-button");
var container = document.getElementById("cart-info");
var payContainer = document.getElementById("pay-info");
var cartWrapper = document.getElementById("cart-wrapper");

function start() {
  getStore(renderStore);
}

start();



//Get store
function getStore(callback) {
  fetch(ordersApi)
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
  fetch(ordersApi, options)
    .then((response) => response.json())
    .then(() => {
      location.reload();
    });
}

function deleteStores(id) {
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
  fetch(ordersApi, options)
    .then((response) => response.json())
    .then(() => {
      // location.reload();
    });
}

//Add purchase
function addPurchase(data) {
	var options = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  };
  fetch(purchaseApi, options)
    .then((response) => response.json())
    .then(() => {
			localStorage.removeItem('total')
			localStorage.removeItem('description')
			overlay.classList.remove("d-none");
			setTimeout(() => {
				overlay.classList.add("d-none");
				payContainer.classList.add("d-none");
				cartWrapper.classList.remove("d-none");
				
				location.reload()
			}, 2000);
		})
} 

//Render store
function renderStore(stores) {
  let string1 = "";
  stores.data.map((store) => {
    if (store.userId == localStorage.getItem("userId")) {
      fetch(foodIdApi + store.foodId)
        .then((response) => response.json())
        .then((content) => {
          var total = store.quantity * content.data.foodPrice;
          string1 += `
      <div class="row container-data">
      <hr />
      <div class="col-2 d-flex align-items-center justify-content-center">
        <p>${content.data.foodName}</p>
      </div>
      <div class="col-2 d-flex align-items-center justify-content-center">
        <p>${store.quantity}</p>
      </div>
      <div class="col-4 d-flex align-items-center justify-content-center">
        <img
          src="${content.data.imageLink}"
          class="img-fluid mb-3 rounded"
          alt="..."
        />
      </div>
      <div
        class="col-2 d-flex align-items-center mb-3 justify-content-center"
      >
        ${total}đ
      </div>
      <div
        class="col-2 d-flex align-items-center mb-3 justify-content-center"
      >
        <button class="btn btn-primary" onclick="deleteStore(${store.id})">Xóa</button>
      </div>
    </div>
  </div>
            `;
          container.innerHTML = string1;
        });

      //     return `
      //     <div class="row container-data">
      //     <hr />
      //     <div class="col-2 d-flex align-items-center justify-content-center">
      //       <p>${foodName}</p>
      //     </div>
      //     <div class="col-2 d-flex align-items-center justify-content-center">
      //       <p>${store.quantity}</p>
      //     </div>
      //     <div class="col-4 d-flex align-items-center justify-content-center">
      //       <img
      //         src="${store.image}"
      //         class="img-fluid mb-3 rounded"
      //         alt="..."
      //       />
      //     </div>
      //     <div
      //       class="col-2 d-flex align-items-center mb-3 justify-content-center"
      //     >
      //       ${store.total}đ
      //     </div>
      //     <div
      //       class="col-2 d-flex align-items-center mb-3 justify-content-center"
      //     >
      //       <button class="btn btn-primary" onclick="deleteStore(${store.id})">Xóa</button>
      //     </div>
      //   </div>
      // </div>
      //           `;
    }
  });
  // container.innerHTML = htmls.join("");
}

// payButton.onclick = () => {
//     container.classList.add('d-none')
//     payContainer.classList.remove('d-none')
// }

// function sumOfPrice(stores) {
//   var sum = 0;
//   stores.map((store) => {
//     sum += store.total;
//   });
//   var htmls = `
//     <h1 class="mt-3">Xác nhận thanh toán</h1>
//     <h3 class="mt-5">Tổng tiền: ${sum}đ</h3>
//     <div class="button-wrap mt-5 mb-3">
//       <button class="btn btn-primary" onclick="confirmPay()">Xác nhận</button>
//       <button class="btn btn-primary ms-2" onclick="backPay()">Trở lại</button>
//     </div>
//   `;
//   payContainer.innerHTML = htmls;
// }

function handleConfirmPay() {
	getUser(renderConfirmPay)
}

function getUser(callback) {
  fetch(getUserApi + localStorage.getItem("userId"))
    .then((response) => response.json())
    .then(callback);
}

function renderConfirmPay(user) {
  var htmls = `
	<div class="header d-flex justify-content-between align-items-center">
	<h1 class="mt-3">Xác nhận thanh toán</h1>
	<button class="btn btn-primary mt-3" id="confirm-button" onclick="handleOnClickConfirm()">
		Xác nhận
	</button>
</div>
<hr />
<div class="d-flex mt-3">
	<label for="fullname" class="h4 me-3">Họ tên người nhận:</label>
	<input type="text" class="form-check" id="fullname" name="fullname" value="${user.data.fullName}"/>
</div>
<div class="d-flex mt-3">
	<label for="address" class="h4 me-3">Địa chỉ nhận:</label>
	<input type="text" class="form-check" id="address" name="address" value="${user.data.address}"/>
</div>
<div class="d-flex mt-3">
	<label for="phoneNumber" class="h4 me-3">Số điện thoại:</label>
	<input
		type="text"
		class="form-check"
		id="phoneNumber"
		name="phoneNumber"
		value="${user.data.phoneNumber}"
	/>
</div>
<div class="d-flex mt-3">
	<label for="" class="h4 me-3">Sản phẩm đặt:</label>
	<p class="h4">${localStorage.getItem('description')}</p>
</div>
<div class="d-flex mt-3">
	<label for="" class="h4 me-3">Tổng tiền:</label>
	<p class="h4">${localStorage.getItem('total')}đ</p>
</div>	
	`;
	payContainer.innerHTML = htmls;
}

//tinh tong tien
fetch(bookFoodApi)
.then((response) => response.json())
.then((content) => {
	var sum = 0;
	content.data.map((data) => {
		sum += data.total
	})
	localStorage.setItem('total', sum)
});

payButton.onclick = () => {
  // fetch(bookFoodApi)
  //   .then((response) => response.json())
  //   .then((content) => {
  //     var sum = 0;
  //     content.data.map((data) => {
  //       sum += data.total
  //     })
  //     localStorage.setItem('total', sum)
  //   });

  payContainer.classList.remove("d-none");
  cartWrapper.classList.add("d-none");
	handleConfirmPay()
};

//click vao xac nhan
function handleOnClickConfirm() {

	fetch(bookFoodApi)
		.then((response) => response.json())
		.then((content) => {
			content.data.map((data) => {
				deleteStores(data.id)
			})
		})

	var fullName = document.querySelector('input[name="fullname"]').value
	var address = document.querySelector('input[name="address"]').value
	var phoneNumber = document.querySelector('input[name="phoneNumber"]').value
	var userId = parseInt(localStorage.getItem('userId'))
	var total = parseInt(localStorage.getItem('total'))
	var description = localStorage.getItem('description')
	var currentdate = new Date();
	var purchaseDate = currentdate.getDate() + '/' + (currentdate.getMonth() + 1) + '/' + currentdate.getFullYear()
	console.log(purchaseDate);

	var data = {
		userId: userId,
		total: total,
		address: address,
		phoneNumber: phoneNumber,
		fullName: fullName,
		description: description,
		purchaseDate: purchaseDate
	}

	addPurchase(data)

}

//Delete all
function deleteAll(stores) {
  stores.map((store) => {
    deleteStore(store.id);
  });
}

function backPay() {
  payContainer.classList.add("d-none");
  cartWrapper.classList.remove("d-none");
}

function confirmPay() {
  getStore(deleteAll);
}
