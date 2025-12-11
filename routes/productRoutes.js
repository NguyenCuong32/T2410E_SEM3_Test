const express = require("express");
const router = express.Router();
const Product = require("../models/Product");

// REST API thêm sản phẩm
router.post("/api/add", async (req, res) => {
    try {
        const product = new Product(req.body);
        await product.save();
        res.json({ message: "Insert product success!", data: product });
    } catch (err) {
        res.status(500).json({ error: err.message });
    }
});

// REST API xóa sản phẩm theo ProductCode
router.delete("/api/delete/:code", async (req, res) => {
    try {
        await Product.deleteOne({ ProductCode: req.params.code });
        res.json({ message: "Delete product success!" });
    } catch (err) {
        res.status(500).json({ error: err.message });
    }
});

module.exports = router;
