const mongoose = require('mongoose');

const productSchema = new mongoose.Schema({
    ProductCode: {
        type: String,
        required: true,
        unique: true
    },
    ProductName: {
        type: String,
        required: true
    },
    ProductDate: {
        type: Date,
        default: Date.now
    },
    ProductOriginPrice: {
        type: Number,
        required: true
    },
    Quantity: {
        type: Number,
        required: true
    },
    ProductStoreCode: {
        type: String,
        required: true
    }
}, { collection: 'ProductCollection' });

module.exports = mongoose.model('Product', productSchema);
