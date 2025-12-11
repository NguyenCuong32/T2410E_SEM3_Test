const express = require("express");
const router = express.Router();
const Product = require("../models/Product");

// GET ALL PRODUCTS
router.get("/", async (req, res) => {
    const products = await Product.find();
    res.render("index", { products });
});

// FORM ADD
router.get("/add", (req, res) => {
    res.render("add");
});

// INSERT PRODUCT
router.post("/add", async (req, res) => {
    const newProduct = new Product(req.body);
    await newProduct.save();
    res.redirect("/");
});

// DELETE PRODUCT
router.get("/delete/:id", async (req, res) => {
    await Product.findByIdAndDelete(req.params.id);
    res.redirect("/");
});

// SORT DESC BY ProductStoreCode
router.get("/sort-storecode", async (req, res) => {
    const products = await Product.find().sort({ ProductStoreCode: -1 });
    res.render("index", { products });
});

module.exports = router;
