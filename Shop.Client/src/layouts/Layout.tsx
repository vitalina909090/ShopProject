import { Outlet } from 'react-router-dom';
import Navbar from '../components/Navbar/Navbar';
import styles from './Layout.module.css';

export default function Layout() {
  return (
    <div className={styles.layout}>
      <Navbar />
      <div className={styles.body}>
        <main className={styles.content}>
          <Outlet />
        </main>
      </div>
    </div>
  );
}