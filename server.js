const express = require("express");
const mongoose = require("mongoose");
const bodyParser = require("body-parser");
const path = require("path");
const Product = require("./models/Product");
const productRoutes = require("./routes/productRoutes");

const app = express();

// Kết nối MongoDB
mongoose.connect("mongodb://127.0.0.1:27017/ProductDB")
    .then(() => console.log("MongoDB connected"))
    .catch(err => console.log(err));

// Middleware
app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.json());
app.use(express.static(path.join(__dirname, "public")));

// Routes API
app.use("/", productRoutes);

// EJS view engine
app.set("view engine", "ejs");

// Trang Home: hiển thị sản phẩm
app.get("/", async (req, res) => {
    const products = await Product.find({});
    res.render("index", { products });
});

// Sort theo ProductStoreCode DESC
app.get("/sort", async (req, res) => {
    const products = await Product.find({}).sort({ ProductStoreCode: -1 });
    res.render("index", { products });
});

// Form thêm sản phẩm
app.get("/add", (req, res) => {
    res.render("add");
});

// Xử lý thêm sản phẩm từ form
app.post("/insert", async (req, res) => {
    await Product.create(req.body);
    res.redirect("/");
});

// Xóa sản phẩm từ giao diện
app.get("/delete/:code", async (req, res) => {
    await Product.deleteOne({ ProductCode: req.params.code });
    res.redirect("/");
});

// Start server
app.listen(3000, () => console.log("Server running at http://localhost:3000"));
