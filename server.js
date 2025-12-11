const express = require("express");
const mongoose = require("mongoose");
const path = require("path");
const expressLayouts = require("express-ejs-layouts");

const productRoute = require("./routes/productRoute");

const app = express();

// Middleware
app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use(express.static(path.join(__dirname, "public")));

// EJS
app.set("view engine", "ejs");
app.set("views", path.join(__dirname, "views"));
app.use(expressLayouts);
app.set("layout", "layout");

// MongoDB
mongoose.connect("mongodb://localhost:27017/ProductDB")
    .then(() => console.log("MongoDB connected!"))
    .catch(err => console.log(err));

// Routes
app.use("/", productRoute);

app.listen(3000, () => console.log("Server running port 3000"));
