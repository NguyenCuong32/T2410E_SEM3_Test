const mongoose = require('mongoose');
const Product = require('./models/Product');

const MONGO_URI = process.env.MONGO_URI || 'mongodb://localhost:27017/productdb';
mongoose.connect(MONGO_URI, { useNewUrlParser: true, useUnifiedTopology: true });

const data = [
  { ProductCode: 'A12', ProductName: 'Product 1', ProductDate: '2023-08-22', ProductOriginPrice: 5000000, Quantity: 1000, ProductStoreCode: 'S10' },
  { ProductCode: 'A13', ProductName: 'Product 2', ProductDate: '2023-07-10', ProductOriginPrice: 2000000, Quantity: 50, ProductStoreCode: 'S10' },
  { ProductCode: 'B20', ProductName: 'Product B', ProductDate: '2023-01-01', ProductOriginPrice: 8000000, Quantity: 10, ProductStoreCode: 'S20' }
];

async function seed(){
  await Product.deleteMany({});
  await Product.insertMany(data);
  console.log('Seed done');
  mongoose.disconnect();
}
seed();
