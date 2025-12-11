product-management/
const express = require("express");
const mongoose = require("mongoose");
const bodyParser = require("body-parser");
const methodOverride = require("method-override");
const productRoutes = require("./routes/productRoutes");

const app = express();

// CONNECT MONGODB
mongoose.connect("mongodb://localhost:27017/ProductDB");

// Middleware
app.use(bodyParser.urlencoded({ extended: true }));
app.use(methodOverride("_method"));
app.set("view engine", "ejs");
app.use(express.static("public"));

// Routes
app.use("/", productRoutes);

app.listen(3000, () => {
  console.log("Server running on port 3000");
});
{
  "name": "product-management",
  "version": "1.0.0",
  "main": "server.js",
  "scripts": {
    "start": "node server.js"
  },
  "dependencies": {
    "body-parser": "^1.20.2",
    "ejs": "^3.1.9",
    "express": "^4.19.0",
    "method-override": "^3.0.0",
    "mongoose": "^7.3.1"
  }
}
const mongoose = require("mongoose");

const ProductSchema = new mongoose.Schema({
  ProductCode: String,
  ProductName: String,
  ProductDate: String,
  ProductOriginPrice: Number,
  Quantity: Number,
  ProductStoreCode: String
});

module.exports = mongoose.model("Product", ProductSchema);
const Product = require("../models/Product");

// Insert product
exports.createProduct = async (req, res) => {
  try {
    const product = new Product(req.body);
    await product.save();

    if (req.originalUrl.includes("/add")) return; 
    res.json({ message: "Product inserted", product });
  } catch (err) {
    res.status(500).json({ error: err });
  }
};

// Delete product
exports.deleteProduct = async (req, res) => {
  try {
    await Product.findByIdAndDelete(req.params.id);

    if (req.originalUrl.includes("/delete")) return;
    res.json({ message: "Product deleted" });
  } catch (err) {
    res.status(500).json({ error: err });
  }
};

// Get all
exports.getAllProducts = async (req, res) => {
  try {
    const products = await Product.find();
    res.json(products);
  } catch (err) {
    res.status(500).json({ error: err });
  }
};

// Sort by ProductStoreCode DESC
exports.getProductsSorted = async () => {
  return await Product.find().sort({ ProductStoreCode: -1 });
};
const express = require("express");
const router = express.Router();
const controller = require("../controllers/productController");

// REST API
router.post("/api/products", controller.createProduct);
router.delete("/api/products/:id", controller.deleteProduct);
router.get("/api/products", controller.getAllProducts);

// Website
router.get("/", async (req, res) => {
  const products = await controller.getProductsSorted();
  res.render("index", { products });
});

router.get("/add", (req, res) => {
  res.render("add_product");
});

router.post("/add", async (req, res) => {
  await controller.createProduct(req, res);
  res.redirect("/");
});

router.get("/delete/:id", async (req, res) => {
  await controller.deleteProduct(req, res);
  res.redirect("/");
});

module.exports = router;
<!DOCTYPE html>
<html>
<head>
  <title>Product Management</title>
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
</head>
<body class="bg-light">
  <div class="container mt-4">
    <%- body %>
  </div>
</body>
</html>
<%- include("layout") %>

<h2 class="mb-4">Product List</h2>

<a href="/add" class="btn btn-primary mb-3">Add New Product</a>

<table class="table table-bordered table-striped">
  <thead>
    <tr>
      <th>Code</th>
      <th>Name</th>
      <th>Date</th>
      <th>Origin Price</th>
      <th>Quantity</th>
      <th>Store Code</th>
      <th>Action</th>
    </tr>
  </thead>
  <tbody>
    <% products.forEach(p => { %>
      <tr>
        <td><%= p.ProductCode %></td>
        <td><%= p.ProductName %></td>
        <td><%= p.ProductDate %></td>
        <td><%= p.ProductOriginPrice.toLocaleString() %></td>
        <td><%= p.Quantity %></td>
        <td><%= p.ProductStoreCode %></td>
        <td>
          <a href="/delete/<%= p._id %>" class="btn btn-danger btn-sm">Delete</a>
        </td>
      </tr>
    <% }) %>
  </tbody>
</table>
<%- include("layout") %>

<h2>Add New Product</h2>

<form action="/add" method="POST" class="mt-3">
  <div class="form-group">
    <label>Product Code</label>
    <input name="ProductCode" class="form-control" required />
  </div>

  <div class="form-group">
    <label>Product Name</label>
    <input name="ProductName" class="form-control" required />
  </div>

  <div class="form-group">
    <label>Product Date</label>
    <input name="ProductDate" class="form-control" required />
  </div>

  <div class="form-group">
    <label>Origin Price</label>
    <input name="ProductOriginPrice" type="number" class="form-control" required />
  </div>

  <div class="form-group">
    <label>Quantity</label>
    <input name="Quantity" type="number" class="form-control" required />
  </div>

  <div class="form-group">
    <label>Store Code</label>
    <input name="ProductStoreCode" class="form-control" required />
  </div>

  <button class="btn btn-success mt-3">Save</button>
</form>
public/css/

HTMLFieldSetElement