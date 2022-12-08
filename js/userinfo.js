//Const
var userApi = "http://localhost:54634/api/Auth";
var getUserApi = "http://localhost:54634/api/Auth/Id?Id="
var saveBtn = document.getElementById("save-user-info");
var container = document.getElementById("user-info-container");

function start() {
	getStore(renderStore);
}

start()

function modifyUser(data) {
  var options = {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  };
  fetch(userApi, options)
    .then((response) => response.json())
    .then(() => {
      overlay.classList.remove("d-none");
      setTimeout(() => {
        overlay.classList.add("d-none");
				location.reload()
      }, 2000);
    });
}

saveBtn.onclick = () => {
  var username = document.querySelector('input[name="name"]').value;
  var fullname = document.querySelector('input[name="fullname"]').value;
  var phoneNumber = document.querySelector('input[name="phone"]').value;
  var address = document.querySelector('input[name="address"]').value;
  var email = document.querySelector('input[name="email"]').value;
  var birth = document.querySelector('input[name="birth"]').value;
  var avatar = document.querySelector('input[name="avatar"]').value;
  var id = localStorage.getItem("userId");

  var data = {
    id: parseInt(id),
    username: username,
    fullname: fullname,
    email: email,
    role: "2",
    address: address,
    phoneNumber: phoneNumber,
    birth: birth,
    avatar: avatar,
  };

  modifyUser(data);
};

//
//Get store
function getStore(callback) {
  fetch(getUserApi + localStorage.getItem('userId'))
    .then((response) => response.json())
    .then(callback)
}

function renderStore(store) {
  var htmls = `
    <div class="row">
<div class="col-6">
  <div class="my-3">
    <label class="mb-0" for="usernameInput">Tên đăng nhập:</label>
    <input
      id="usernameInput"
      type="text"
      class="form-control input-group"
      name="name"
      value="${store.data.userName}"
    />
  </div>
	<div class="my-3">
    <label class="mb-0" for="fullnameInput">Họ và tên:</label>
    <input
      id="fullnameInput"
      type="text"
      class="form-control input-group"
      name="fullname"
			value="${store.data.fullName}"
    />
  </div>
  <div class="my-3">
    <label class="mb-0" for="phoneInput">Số điện thoại:</label>
    <input
      id="phoneInput"
      type="email"
      class="form-control input-group"
      name="phone"
			value="${store.data.phoneNumber}"
    />
  </div>
  <div class="my-3">
    <label class="mb-0" for="addressInput">Địa chỉ:</label>
    <input
      id="addressInput"
      type="text"
      class="form-control input-group"
      name="address"
			value="${store.data.address}"
    />
  </div>
  <div class="my-3">
    <label class="mb-0" for="emailInput">Email:</label>
    <input
      id="emailInput"
      type="text"
      class="form-control input-group"
      name="email"
			value="${store.data.email}"
    />
  </div>
  <div class="my-3">
    <label class="mb-0" for="birthInput">Ngày sinh:</label>
    <input
      id="birthInput"
      type="date"
      class="form-control input-group"
      name="birth"
			value="${store.data.birth}"
    />
  </div>
  <div class="my-3">
    <label class="mb-0" for="avatarInput">Ảnh đại diện:</label>
    <input
      id="avatarInput"
      type="text"
      class="form-control input-group"
      name="avatar"
      placeholder="Điền link hình ảnh"
			value="${store.data.avatar}"
    />
  </div>
</div>
<div class="col-6 d-flex justify-content-center align-items-center">
  <img src="${store.data.avatar}" alt="" class=" rounded-circle" width="300px" height="300px">
</div>
    `;
		container.innerHTML = htmls;
  };
