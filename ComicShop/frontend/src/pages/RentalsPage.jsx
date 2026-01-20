import React, { useEffect, useState } from 'react';
import { rentalService } from '../services/rentalService';
import { Clock, Plus, User } from 'lucide-react';
import styles from './RentalsPage.module.css';

const RentalsPage = () => {
    const [rentals, setRentals] = useState([]);
    const [loading, setLoading] = useState(true);
    const [showModal, setShowModal] = useState(false);
    const [newRental, setNewRental] = useState({ customerId: '', comicBookId: '' });

    useEffect(() => {
        fetchRentals();
    }, []);

    const fetchRentals = async () => {
        try {
            const data = await rentalService.getAll();
            setRentals(data);
        } catch (error) {
            console.error('Failed to fetch rentals');
        } finally {
            setLoading(false);
        }
    };

    const handleCreate = async (e) => {
        e.preventDefault();
        try {
            const payload = {
                customerId: parseInt(newRental.customerId),
                rentalDetails: [{ comicBookId: parseInt(newRental.comicBookId), quantity: 1 }] // Simplified for single book
            };
            await rentalService.createRental(payload);
            setShowModal(false);
            fetchRentals();
            setNewRental({ customerId: '', comicBookId: '' });
        } catch (error) {
            alert('Failed to create rental');
        }
    };

    return (
        <div className="container">
            <div className={styles.header}>
                <h1>Rentals Management</h1>
                <button className="btn-primary" onClick={() => setShowModal(true)}>
                    <Plus size={18} style={{ marginRight: '8px' }} /> New Rental
                </button>
            </div>

            {loading ? <p>Loading...</p> : (
                <div className={`glass-panel ${styles.tableContainer}`}>
                    <table className={styles.table}>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Customer</th>
                                <th>Date</th>
                                <th>Return Date</th>
                                <th>Status</th>
                            </tr>
                        </thead>
                        <tbody>
                            {rentals.map((rental) => (
                                <tr key={rental.rentalid || rental.id}>
                                    <td>#{rental.rentalid || rental.id}</td>
                                    <td>{rental.customer?.fullname || rental.customerId}</td>
                                    <td>{new Date(rental.rentaldate).toLocaleDateString()}</td>
                                    <td>{new Date(rental.returndate).toLocaleDateString()}</td>
                                    <td>
                                        <span className={`${styles.status} ${styles[rental.status.toLowerCase()]}`}>
                                            {rental.status}
                                        </span>
                                    </td>
                                </tr>
                            ))}
                            {rentals.length === 0 && <tr><td colSpan="5" align="center">No active rentals</td></tr>}
                        </tbody>
                    </table>
                </div>
            )}

            {showModal && (
                <div className={styles.modalOverlay}>
                    <div className={`glass-panel ${styles.modal}`}>
                        <h2>New Rental</h2>
                        <form onSubmit={handleCreate}>
                            <div className={styles.field}>
                                <label>Customer ID</label>
                                <input
                                    type="number"
                                    value={newRental.customerId}
                                    onChange={e => setNewRental({ ...newRental, customerId: e.target.value })}
                                    required
                                />
                            </div>
                            <div className={styles.field}>
                                <label>ComicBook ID</label>
                                <input
                                    type="number"
                                    value={newRental.comicBookId}
                                    onChange={e => setNewRental({ ...newRental, comicBookId: e.target.value })}
                                    required
                                />
                            </div>
                            <div className={styles.modalActions}>
                                <button type="button" onClick={() => setShowModal(false)} className={styles.cancelBtn}>Cancel</button>
                                <button type="submit" className="btn-primary">Create</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default RentalsPage;
