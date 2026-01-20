import React, { useState } from 'react';
import { customerService } from '../services/customerService';
import { UserPlus, User, Phone } from 'lucide-react';
import styles from './CustomerRegisterPage.module.css';

const CustomerRegisterPage = () => {
    const [formData, setFormData] = useState({
        fullname: '',
        phonenumber: ''
    });
    const [status, setStatus] = useState({ type: '', message: '' });

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await customerService.register(formData);
            setStatus({ type: 'success', message: 'Customer registered successfully!' });
            setFormData({ fullname: '', phonenumber: '' });
        } catch (error) {
            setStatus({ type: 'error', message: 'Registration failed. Try again.' });
        }
    };

    return (
        <div className="container">
            <div className={styles.wrapper}>
                <div className={`glass-panel ${styles.formCard}`}>
                    <div className={styles.header}>
                        <UserPlus size={48} className={styles.icon} />
                        <h2>New Customer</h2>
                        <p>Register a new member to the comic club</p>
                    </div>

                    {status.message && (
                        <div className={`${styles.alert} ${styles[status.type]}`}>
                            {status.message}
                        </div>
                    )}

                    <form onSubmit={handleSubmit}>
                        <div className={styles.inputGroup}>
                            <label>Full Name</label>
                            <div className={styles.inputWrapper}>
                                <User size={18} />
                                <input
                                    type="text"
                                    placeholder="John Doe"
                                    value={formData.fullname}
                                    onChange={(e) => setFormData({ ...formData, fullname: e.target.value })}
                                    required
                                />
                            </div>
                        </div>

                        <div className={styles.inputGroup}>
                            <label>Phone Number</label>
                            <div className={styles.inputWrapper}>
                                <Phone size={18} />
                                <input
                                    type="tel"
                                    placeholder="+1 234 567 890"
                                    value={formData.phonenumber}
                                    onChange={(e) => setFormData({ ...formData, phonenumber: e.target.value })}
                                    required
                                />
                            </div>
                        </div>

                        <button type="submit" className="btn-primary" style={{ width: '100%', marginTop: '1rem' }}>
                            Register Customer
                        </button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default CustomerRegisterPage;
