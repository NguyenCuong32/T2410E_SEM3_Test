const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const path = require('path');
const methodOverride = require('method-override');
require('dotenv').config();

const app = express();
const PORT = process.env.PORT || 3000;


const mongoURI = process.env.MONGODB_URI || 'mongodb://localhost:27017/product-manage-db';
mongoose.connect(mongoURI)
    .then(() => console.log('MongoDB Connected'))
    .catch(err => console.log(err));


app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use(methodOverride('_method'));
app.use(express.static('public'));

app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

const apiRoutes = require('./routes/api');
const webRoutes = require('./routes/web');

app.use('/api', apiRoutes);
app.use('/', webRoutes);

app.listen(PORT, () => {
    console.log(`Server running on http://localhost:${PORT}`);
});
