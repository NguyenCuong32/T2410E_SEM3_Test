const express = require("express");
const mongoose = require("mongoose");
const bodyParser = require("body-parser");
const path = require("path");
const Product = require("./models/Product");
const productRoutes = require("./routes/productRoutes");

const app = express();

// MongoDB connection
mongoose.connect("mongodb://127.0.0.1:27017/NewProductDB")
    .then(() => console.log("Connected to MongoDB"))
    .catch(err => console.log(err));

app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.json());
app.use(express.static(path.join(__dirname, "public")));

app.set("view engine", "ejs");

// API routes
app.use("/", productRoutes);

// Load homepage
app.get("/", async (req, res) => {
    const data = await Product.find({});
    res.render("index", { list: data });
});

// Form create
app.get("/add", (req, res) => {
    res.render("add");
});

// Handle form submit
app.post("/create", async (req, res) => {
    await Product.create(req.body);
    res.redirect("/");
});

// Delete product
app.get("/delete/:code", async (req, res) => {
    await Product.deleteOne({ code: req.params.code });
    res.redirect("/");
});

app.listen(3001, () => {
    console.log("Server running at http://localhost:3001");
});
