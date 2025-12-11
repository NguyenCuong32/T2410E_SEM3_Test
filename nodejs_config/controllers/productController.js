const Product = require('../models/Product');
exports.createProduct = async (req, res) => {
    try {
        const { ProductCode, ProductName, ProductDate, ProductOriginPrice, Quantity, ProductStoreCode } = req.body;

        if (!ProductCode || !ProductName || !ProductOriginPrice) {
            return res.status(400).json({ message: 'Please provide required fields' });
        }

        const newProduct = new Product({
            ProductCode,
            ProductName,
            ProductDate,
            ProductOriginPrice,
            Quantity,
            ProductStoreCode
        });

        const savedProduct = await newProduct.save();
        res.status(201).json({ message: 'Product created successfully', data: savedProduct });
    } catch (err) {
        res.status(500).json({ message: 'Error creating product', error: err.message });
    }
};

exports.deleteProduct = async (req, res) => {
    try {
        const { id } = req.params; 

        const deletedProduct = await Product.findByIdAndDelete(id);

        if (!deletedProduct) {
            return res.status(404).json({ message: 'Product not found' });
        }

        res.status(200).json({ message: 'Product deleted successfully' });
    } catch (err) {
        res.status(500).json({ message: 'Error deleting product', error: err.message });
    }
};

exports.getAllProducts = async (req, res) => {
    try {

        const products = await Product.find().sort({ ProductStoreCode: -1 });

        res.status(200).json({ count: products.length, data: products });
    } catch (err) {
        res.status(500).json({ message: 'Error fetching products', error: err.message });
    }
};
