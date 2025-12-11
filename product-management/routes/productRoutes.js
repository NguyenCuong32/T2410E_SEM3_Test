// routes/productRoutes.js
const express = require('express');
const router = express.Router();
const ctrl = require('../controllers/productController');

// REST API
router.get('/api/products', ctrl.apiGetAll);
router.post('/api/products', ctrl.apiCreate);
router.delete('/api/products/:id', ctrl.apiDelete);

// Web
router.get('/', ctrl.webGetAll);
router.get('/add', ctrl.webAddForm);
router.post('/add', ctrl.webCreate);
router.get('/delete/:id', ctrl.webDelete);

module.exports = router;