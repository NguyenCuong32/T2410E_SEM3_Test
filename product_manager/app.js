const express = require("express");
const mongoose = require("mongoose");
const path = require("path");

const app = express();

// view engine
app.set("view engine", "ejs");
app.set("views", path.join(__dirname, "views"));

app.use(express.urlencoded({ extended: true }));
app.use(express.json());

// Mongo
mongoose
  .connect("mongodb://127.0.0.1:27017/productdb")
  .then(() => console.log("MongoDB connected"))
  .catch((err) => console.error(err));

// Routes
const productRoutes = require("./routes/productRoutes");
app.use(productRoutes);

app.get("/", (req, res) => res.redirect("/products"));

app.listen(3000, () => console.log("Server running at http://localhost:3000"));
