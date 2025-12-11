import React from 'react'

const ProductList = ({ products, onProductDeleted }) => {
    const handleDelete = async (id) => {
        if (window.confirm('Are you sure you want to delete this product?')) {
            try {
                const response = await fetch(`http://localhost:3000/api/products/${id}`, {
                    method: 'DELETE',
                })
                if (response.ok) {
                    onProductDeleted()
                } else {
                    alert('Failed to delete product')
                }
            } catch (error) {
                console.error('Error deleting product:', error)
                alert('Error deleting product')
            }
        }
    }


    return (
        <div className="card">
            <div className="card-header bg-primary text-white">
                <h5 className="mb-0">Product List</h5>
            </div>
            <div className="card-body">
                {products.length === 0 ? (
                    <p className="text-center">No products found.</p>
                ) : (
                    <div className="table-responsive">
                        <table className="table table-striped table-hover">
                            <thead>
                                <tr>
                                    <th>Code</th>
                                    <th>Name</th>
                                    <th>Date</th>
                                    <th>Price</th>
                                    <th>Quantity</th>
                                    <th>Store Code</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                {products.map((product) => (
                                    <tr key={product._id}>
                                        <td>{product.ProductCode}</td>
                                        <td>{product.ProductName}</td>
                                        <td>{product.ProductDate}</td>
                                        <td>{product.ProductOriginPrice.toLocaleString()}</td>
                                        <td>{product.Quantity}</td>
                                        <td>{product.ProductStoreCode}</td>
                                        <td>
                                            <button
                                                className="btn btn-danger btn-sm"
                                                onClick={() => handleDelete(product._id)}
                                            >
                                                Delete
                                            </button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    )
}

export default ProductList
