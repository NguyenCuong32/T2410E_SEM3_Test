// Ví dụ: thông báo khi thêm sản phẩm
document.addEventListener("DOMContentLoaded", () => {
  const form = document.querySelector("form[action='/add']");
  if (form) {
    form.addEventListener("submit", () => {
      alert("Đang thêm sản phẩm mới...");
    });
  }
});