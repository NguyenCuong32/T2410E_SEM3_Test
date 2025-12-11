const express = require('express');
const mongoose = require('mongoose');
const body = require('body-parser');
const app = express();

// CONNECT MONGODB
mongoose.connect("mongodb://127.0.0.1:27017/ProductDB")
    .then(() => console.log("Connected to MongoDB"))
    .catch(err => console.log(err));

// Middleware
app.use(body.urlencoded({ extended: true }));
app.set("view engine", "ejs");

// Redirect root path
app.get("/", (req, res) => {
    res.redirect("/web");
});

// API routes
app.use('/api/product', require('./routes/productApi'));

// Web routes
app.use('/web', require('./routes/productWeb'));

app.listen(3000, () => console.log("Server running on http://localhost:3000"));
