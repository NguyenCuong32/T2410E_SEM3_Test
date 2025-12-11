import React, { useState } from 'react'

const ProductForm = ({ onProductAdded }) => {
    const [formData, setFormData] = useState({
        ProductCode: '',
        ProductName: '',
        ProductDate: '',
        ProductOriginPrice: '',
        Quantity: '',
        ProductStoreCode: ''
    })

    const handleChange = (e) => {
        const { name, value } = e.target
        setFormData({
            ...formData,
            [name]: value
        })
    }

    const handleSubmit = async (e) => {
        e.preventDefault()
        try {
            const response = await fetch('http://localhost:3000/api/products', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formData)
            })

            if (response.ok) {
                alert('Product added successfully')
                setFormData({
                    ProductCode: '',
                    ProductName: '',
                    ProductDate: '',
                    ProductOriginPrice: '',
                    Quantity: '',
                    ProductStoreCode: ''
                })
                onProductAdded()
            } else {
                const errorData = await response.json()
                alert(`Failed to add product: ${errorData.message}`)
            }
        } catch (error) {
            console.error('Error adding product:', error)
            alert('Error adding product')
        }
    }

    return (
        <div className="card mb-4">
            <div className="card-header bg-success text-white">
                <h5 className="mb-0">Add New Product</h5>
            </div>
            <div className="card-body">
                <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                        <label className="form-label">Product Code</label>
                        <input
                            type="text"
                            className="form-control"
                            name="ProductCode"
                            value={formData.ProductCode}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Product Name</label>
                        <input
                            type="text"
                            className="form-control"
                            name="ProductName"
                            value={formData.ProductName}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Product Date</label>
                        <input
                            type="date" 
                            className="form-control"
                            name="ProductDate"
                            value={formData.ProductDate}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Origin Price</label>
                        <input
                            type="number"
                            className="form-control"
                            name="ProductOriginPrice"
                            value={formData.ProductOriginPrice}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Quantity</label>
                        <input
                            type="number"
                            className="form-control"
                            name="Quantity"
                            value={formData.Quantity}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Store Code</label>
                        <input
                            type="text"
                            className="form-control"
                            name="ProductStoreCode"
                            value={formData.ProductStoreCode}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <button type="submit" className="btn btn-primary w-100">Add Product</button>
                </form>
            </div>
        </div>
    )
}

export default ProductForm
