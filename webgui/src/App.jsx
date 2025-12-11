import { useState, useEffect } from 'react'
import ProductList from './components/ProductList'
import ProductForm from './components/ProductForm'

function App() {
  const [products, setProducts] = useState([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)

  const fetchProducts = async () => {
    setLoading(true)
    try {
      const response = await fetch('http://localhost:3000/api/products')
      if (!response.ok) {
        throw new Error('Failed to fetch products')
      }
      const data = await response.json()
      setProducts(data.data)
      setError(null)
    } catch (err) {
      setError(err.message)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    fetchProducts()
  }, [])

  const handleProductAdded = () => {
    fetchProducts()
  }

  const handleProductDeleted = () => {
    fetchProducts()
  }

  return (
    <div className="container mt-5">
      <h1 className="text-center mb-4">Product Management System</h1>
      <div className="row">
        <div className="col-md-4">
          <ProductForm onProductAdded={handleProductAdded} />
        </div>
        <div className="col-md-8">
          {error && <div className="alert alert-danger">{error}</div>}
          {loading ? (
            <div className="text-center">Loading...</div>
          ) : (
            <ProductList products={products} onProductDeleted={handleProductDeleted} />
          )}
        </div>
      </div>
    </div>
  )
}

export default App
