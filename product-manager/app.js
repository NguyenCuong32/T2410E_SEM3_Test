const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const methodOverride = require('method-override');
const path = require('path');

const apiRoutes = require('./routes/api');
const webRoutes = require('./routes/web');

const app = express();


app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

app.use(express.static(path.join(__dirname, 'public')));
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use(methodOverride('_method')); 

const MONGO_URI = process.env.MONGO_URI || 'mongodb://localhost:27017/productdb';
mongoose.connect(MONGO_URI, {
  useNewUrlParser: true,
  useUnifiedTopology: true
}).then(()=>console.log('MongoDB connected'))
  .catch(err => console.error('MongoDB connect error', err));

app.use('/api', apiRoutes);
app.use('/', webRoutes);

app.use((req, res) => res.status(404).send('Not Found'));

const PORT = process.env.PORT || 3000;
app.listen(PORT, ()=> console.log(`Server running on http://localhost:${PORT}`));
