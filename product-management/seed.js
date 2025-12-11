const mongoose = require('mongoose');
const Product = require('./models/Product');
require('dotenv').config();

mongoose.connect(process.env.MONGODB_URI);

const sample = {
  ProductCode: "A12",
  ProductName: "Product 1",
  ProductDate: new Date('2023-08-22'),
  ProductOriginPrice: 5000000,
  Quantity: 1000,
  ProductStoreCode: "S10"
};

Product.insertMany([sample])
  .then(() => {
    console.log('Inserted');
    mongoose.connection.close();
  })
  .catch(err => console.log(err));