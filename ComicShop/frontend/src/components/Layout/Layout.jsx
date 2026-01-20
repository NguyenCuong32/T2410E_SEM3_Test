import React from 'react';
import Navbar from './Navbar';
import styles from './Layout.module.css';

const Layout = ({ children }) => {
    return (
        <div className={styles.layout}>
            <Navbar />
            <main className={styles.main}>
                {children}
            </main>
            <footer className={styles.footer}>
                <p>&copy; 2026 ComicShop. All rights reserved.</p>
            </footer>
        </div>
    );
};

export default Layout;
