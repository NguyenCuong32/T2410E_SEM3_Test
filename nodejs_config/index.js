const express = require('express');
const bodyParser = require('body-parser');
const connectDB = require('./config/database');
const productRoutes = require('./routes/productRoutes');
require('dotenv').config();

const app = express();
const PORT = process.env.PORT || 3000;

connectDB();

const cors = require('cors');

app.use(cors());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

app.use('/api', productRoutes);

app.get('/', (req, res) => {
    res.send('Product Management API is running');
});

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
