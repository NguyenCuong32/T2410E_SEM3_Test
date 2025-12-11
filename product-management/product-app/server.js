const express = require("express");
const bodyParser = require("body-parser");
const path = require("path");

const app = express();

app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.static("public"));

app.set("view engine", "ejs");

// Routes
const webRoutes = require("./routes/web");
const apiRoutes = require("./routes/api");

app.use("/", webRoutes);
app.use("/api", apiRoutes);

app.listen(3000, () => {
    console.log("Server running on http://localhost:3000");
});
