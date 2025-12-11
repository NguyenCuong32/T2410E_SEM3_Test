const http = require('http');

const postData = JSON.stringify({
    ProductCode: "A12",
    ProductName: "Product 1",
    ProductDate: "2023-08-22",
    ProductOriginPrice: 5000000,
    Quantity: 1000,
    ProductStoreCode: "S10"
});

const options = {
    hostname: 'localhost',
    port: 3000,
    path: '/api/products',
    method: 'POST',
    headers: {
        'Content-Type': 'application/json',
        'Content-Length': postData.length
    }
};

const req = http.request(options, (res) => {
    console.log(`POST STATUS: ${res.statusCode}`);
    let data = '';
    res.on('data', (chunk) => { data += chunk; });
    res.on('end', () => {
        console.log('POST Response:', data);
        try {
            const product = JSON.parse(data);
            if (product._id) {
                deleteProduct(product._id);
            }
        } catch (e) {
            console.error('Error parsing response:', e);
        }
    });
});

req.on('error', (e) => {
    console.error(`problem with request: ${e.message}`);
});

req.write(postData);
req.end();

function deleteProduct(id) {
    const delOptions = {
        hostname: 'localhost',
        port: 3000,
        path: `/api/products/${id}`,
        method: 'DELETE'
    };

    const delReq = http.request(delOptions, (res) => {
        console.log(`DELETE STATUS: ${res.statusCode}`);
        res.on('data', (d) => { process.stdout.write(d); });
    });
    delReq.on('error', (e) => {
        console.error(`delete error: ${e.message}`);
    });
    delReq.end();
}
