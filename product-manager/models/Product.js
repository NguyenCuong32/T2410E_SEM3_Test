const mongoose = require('mongoose');

const ProductSchema = new mongoose.Schema({
  ProductCode: { type: String, required: true, unique: true },
  ProductName: { type: String, required: true },
  ProductDate: { type: Date }, 
  ProductOriginPrice: { type: Number, required: true },
  Quantity: { type: Number, default: 0 },
  ProductStoreCode: { type: String, required: true }
}, { timestamps: true });

module.exports = mongoose.model('Product', ProductSchema);
