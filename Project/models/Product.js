const mongoose = require('mongoose');

const ProductSchema = new mongoose.Schema({
    ProductCode: { type: String, required: true },
    ProductName: { type: String, required: true },
    ProductDate: { type: Date, required: true },
    ProductOriginPrice: { type: Number, required: true },
    Quantity: { type: Number, required: true },
    ProductStoreCode: { type: String, required: true }
});

module.exports = mongoose.model('Product', ProductSchema);
