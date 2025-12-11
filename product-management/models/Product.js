const mongoose = require('mongoose');

const ProductSchema = new mongoose.Schema({
    ProductCode: String,
    ProductName: String,
    ProductDate: String,
    ProductOriginPrice: Number,
    Quantity: Number,
    ProductStoreCode: String
});

module.exports = mongoose.model('Product', ProductSchema);
