const mongoose = require("mongoose");

const ProductSchema = new mongoose.Schema({
    code: { type: String, required: true, unique: true },
    name: { type: String, required: true },
    date: { type: String },
    originPrice: { type: Number, default: 0 },
    quantity: { type: Number, default: 0 },
    storeCode: { type: String }
});

module.exports = mongoose.model("Products", ProductSchema);