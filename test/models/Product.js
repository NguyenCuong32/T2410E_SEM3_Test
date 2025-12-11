const mongoose = require('mongoose');


const productSchema = new mongoose.Schema({
    ProductCode: { type: String, required: true },      
    ProductName: { type: String, required: true },      
    ProductDate: { type: String },                     
    ProductOriginPrice: { type: Number },                
    Quantity: { type: Number },                          
    ProductStoreCode: { type: String, required: true }   
});


module.exports = mongoose.model('Product', productSchema, 'ProductCollection');