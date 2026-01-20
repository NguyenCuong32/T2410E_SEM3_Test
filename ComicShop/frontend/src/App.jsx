import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout/Layout';

import HomePage from './pages/HomePage';
import RentalsPage from './pages/RentalsPage';
import CustomerRegisterPage from './pages/CustomerRegisterPage';

function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/rentals" element={<RentalsPage />} />
          <Route path="/customers" element={<CustomerRegisterPage />} />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;
