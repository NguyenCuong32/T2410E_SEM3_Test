const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const Product = require('./models/Product'); 

const app = express();


app.set('view engine', 'ejs');


app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());


mongoose.connect('mongodb://127.0.0.1:27017/ProductDB')
    .then(() => console.log("Đã kết nối MongoDB thành công"))
    .catch(err => console.error("Lỗi kết nối MongoDB:", err));



app.post('/api/products', async (req, res) => {
    try {
        const newProduct = new Product(req.body);
        await newProduct.save();
        res.status(200).json({ 
            message: "Thêm sản phẩm thành công", 
            data: newProduct 
        });
    } catch (err) {
        res.status(500).json({ error: err.message });
    }
});


app.delete('/api/products/:id', async (req, res) => {
    try {
        await Product.findByIdAndDelete(req.params.id);
        res.status(200).json({ message: "Xóa sản phẩm thành công" });
    } catch (err) {
        res.status(500).json({ error: err.message });
    }
});


app.get('/', async (req, res) => {
    try {
        // .sort({ ProductStoreCode: -1 }) nghĩa là sắp xếp giảm dần
        const products = await Product.find().sort({ ProductStoreCode: -1 });
        
        // Render file views/index.ejs và truyền dữ liệu products qua
        res.render('index', { products: products });
    } catch (err) {
        res.send("Lỗi lấy dữ liệu: " + err.message);
    }
});

app.post('/web/add', async (req, res) => {
    try {
        const newProduct = new Product({
            ProductCode: req.body.ProductCode,
            ProductName: req.body.ProductName,
            ProductDate: req.body.ProductDate,
            ProductOriginPrice: req.body.ProductOriginPrice,
            Quantity: req.body.Quantity,
            ProductStoreCode: req.body.ProductStoreCode
        });
        await newProduct.save();
        res.redirect('/'); 
    } catch (err) {
        res.send("Lỗi thêm mới: " + err.message);
    }
});


app.post('/web/delete/:id', async (req, res) => {
    try {
        await Product.findByIdAndDelete(req.params.id);
        res.redirect('/'); 
    } catch (err) {
        res.send("Lỗi xóa: " + err.message);
    }
});


app.listen(3000, () => {
    console.log('Server đang chạy tại: http://localhost:3000');
});