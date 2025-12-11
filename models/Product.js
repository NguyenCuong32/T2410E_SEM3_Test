const mongoose = require('mongoose');

const productSchema = new mongoose.Schema({
  ProductCode: { type: String, required: true, unique: true },
  ProductName: { type: String, required: true },
  ProductDate: { type: String, required: true }, // có thể dùng Date nếu muốn
  ProductOriginPrice: { type: Number, required: true },
  Quantity: { type: Number, required: true },
  ProductStoreCode: { type: String, required: true }
}, { collection: 'ProductCollection' });

module.exports = mongoose.model('Product', productSchema);