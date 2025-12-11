const express = require("express");
const router = express.Router();
const Product = require("../models/Product");

// REST: insert
router.post("/api/products", async (req, res) => {
  try {
    await Product.create(req.body);
    res.json({ message: "Inserted" });
  } catch (err) {
    res.status(500).json({ error: err });
  }
});

// REST: delete
router.delete("/api/products/:id", async (req, res) => {
  await Product.findByIdAndDelete(req.params.id);
  res.json({ message: "Deleted" });
});

// Website: list + sort
router.get("/products", async (req, res) => {
  const products = await Product.find().sort({
    ProductStoreCode: 1,
    ProductOriginPrice: -1,
  });
  res.render("products/index", { products });
});

// Website: insert
router.post("/products", async (req, res) => {
  await Product.create(req.body);
  res.redirect("/products");
});

// Website: delete
router.post("/products/:id/delete", async (req, res) => {
  await Product.findByIdAndDelete(req.params.id);
  res.redirect("/products");
});

module.exports = router;
