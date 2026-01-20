import React from 'react';
import { Link } from 'react-router-dom';
import { ShoppingBag, BookOpen, Users, LogIn } from 'lucide-react';
import styles from './Navbar.module.css';

const Navbar = () => {
    return (
        <nav className={styles.navbar}>
            <div className={styles.container}>
                <Link to="/" className={styles.logo}>
                    <BookOpen className={styles.icon} />
                    <span>ComicShop</span>
                </Link>
                <ul className={styles.navLinks}>
                    <li><Link to="/">ComicBooks</Link></li>
                    <li><Link to="/rentals">Rentals</Link></li>
                    <li><Link to="/customers">Customers</Link></li>
                </ul>
                <div className={styles.actions}>
                    <Link to="/login" className="btn-primary">
                        <LogIn size={18} style={{ marginRight: '8px' }} /> Login
                    </Link>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;
