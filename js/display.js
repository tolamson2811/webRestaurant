var navContainer = document.getElementById("nav-container");
var username = localStorage.getItem("username");
var getUserApi = "http://localhost:54634/api/Auth/Id?Id=";

function start() {
  getStore(renderStore);
}

function getStore(callback) {
  fetch(getUserApi + localStorage.getItem("userId"))
    .then((response) => response.json())
    .then(callback);
}

function renderStore(store) {
  var htmls = `
  <div class="container">
  <a href="index.html" class="navbar-brand">
    <img
      src="https://media.istockphoto.com/id/981368726/vector/restaurant-food-drinks-logo-fork-knife-background-vector-image.jpg?s=612x612&w=0&k=20&c=9M26CBkCyEBqUPs3Ls5QCjYLZrB9sxwrSFmnAmNCopI="
      alt=""
      width="60"
      height="50"
      class="rounded-circle"
    />
  </a>
  <button
    class="navbar-toggler"
    type="button"
    data-bs-toggle="collapse"
    data-bs-target="#navbarSupportedContent"
    aria-controls="navbarSupportedContent"
    aria-expanded="false"
    aria-label="Toggle navigation"
  >
    <span class="navbar-toggler-icon"></span>
  </button>
  <div class="collapse navbar-collapse" id="navbarSupportedContent">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0 h6">
      <li class="nav-item active">
        <a href="index.html" class="nav-link">Trang chủ</a>
      </li>
      <li class="nav-item">
        <a href="userinfo.html" class="nav-link">Thông tin cá nhân</a>
      </li>
      <li class="nav-item">
        <a href="cart.html" class="nav-link">Giỏ hàng</a>
      </li>
      <li class="nav-item">
        <a href="userHistory.html" class="nav-link">Lịch sử mua hàng</a>
      </li>
    </ul>
    <div class="d-flex justify-content-center align-items-center" id="user-logined">
      <div>
        <img src="${store.data.avatar}" alt="" class="rounded-circle me-2" width="30px" height="30px">
        ${store.data.userName}
      </div>
      <a href="login.html" class="nav-link fw-bold ms-3" id="log-out-btn">Đăng xuất</a>
    </div>
  </div>
</div>
      `;
  navContainer.innerHTML = htmls;
}

if (localStorage.getItem("username") === null) {
  var htmls = `
    <div class="container">
    <a href="index.html" class="navbar-brand">
      <img
        src="https://media.istockphoto.com/id/981368726/vector/restaurant-food-drinks-logo-fork-knife-background-vector-image.jpg?s=612x612&w=0&k=20&c=9M26CBkCyEBqUPs3Ls5QCjYLZrB9sxwrSFmnAmNCopI="
        alt=""
        width="60"
        height="50"
        class="rounded-circle"
      />
    </a>
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarSupportedContent"
      aria-controls="navbarSupportedContent"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0 h6">
        <li class="nav-item active">
          <a href="index.html" class="nav-link">Trang chủ</a>
        </li>
      </ul>
      <div class="d-flex justify-content-center align-items-center" id="user-none-login">
        <a href="register.html" class="nav-link fw-bold">Đăng ký</a>
        <a href="login.html" class="nav-link fw-bold">Đăng nhập</a>
      </div>
    </div>
  </div>
    `;
  navContainer.innerHTML = htmls;
} else if (localStorage.getItem("userRole") == 2) {
  start();
  var logOutBtn = document.getElementById("log-out-btn");
  logOutBtn.onclick = () => {
    console.log('dkm');
    localStorage.removeItem("userId");
    localStorage.removeItem("username");
    localStorage.removeItem("userRole");
    location.replace("http://127.0.0.1:5500/login.html");
  };
} else {
  var htmls = `
    <div class="container">
        <a href="index.html" class="navbar-brand">
          <img
            src="https://media.istockphoto.com/id/981368726/vector/restaurant-food-drinks-logo-fork-knife-background-vector-image.jpg?s=612x612&w=0&k=20&c=9M26CBkCyEBqUPs3Ls5QCjYLZrB9sxwrSFmnAmNCopI="
            alt=""
            width="60"
            height="50"
            class="rounded-circle"
          />
        </a>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0 h6">
            <li class="nav-item active">
              <a href="index.html" class="nav-link">Trang chủ</a>
            </li>
            <li class="nav-item">
              <a href="dashboard.html" class="nav-link">Quản lí</a>
            </li>
            <li class="nav-item">
              <a href="userHistory.html" class="nav-link">Đơn đặt hàng</a>
            </li>
          </ul>
          <div class="d-flex justify-content-center align-items-center" id="user-logined">
            <div>
              <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-person-circle mb-1" viewBox="0 0 16 16">
                <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0z"/>
                <path fill-rule="evenodd" d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8zm8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1z"/>
              </svg>
              ${username}
            </div>
            <a href="login.html" class="nav-link fw-bold ms-3" id="log-out-btn">Đăng xuất</a>
          </div>
        </div>
      </div>    
    `;
  navContainer.innerHTML = htmls;
  var logOutBtn = document.getElementById("log-out-btn");
  logOutBtn.onclick = () => {
    localStorage.removeItem("userId");
    localStorage.removeItem("username");
    localStorage.removeItem("userRole");
    location.replace("http://127.0.0.1:5500/login.html");
  };
}
