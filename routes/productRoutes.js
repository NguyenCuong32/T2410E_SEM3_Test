const express = require("express");
const router = express.Router();
const Product = require("../models/Product");

// API thêm sản phẩm
router.post("/api/products", async (req, res) => {
    try {
        const newProduct = new Product(req.body);
        await newProduct.save();
        res.json({ message: "Product created successfully", product: newProduct });
    } catch (error) {
        res.status(400).json({ error: error.message });
    }
});

// API xóa sản phẩm
router.delete("/api/products/:code", async (req, res) => {
    try {
        await Product.deleteOne({ code: req.params.code });
        res.json({ message: "Product removed" });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

module.exports = router;