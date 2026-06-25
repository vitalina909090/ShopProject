import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
  faClipboardList,
  faShoppingCart,
  faHeart,
  faBell,
  faComments,
  faUser,
} from '@fortawesome/free-solid-svg-icons';
import styles from './Navbar.module.css';

export default function Navbar() {
  return (
    <nav className={styles.navbar}>
      <div className={styles.innerNavbar}>
        <Link to="/" className={styles.logo}>MYSHOP</Link>

        <div className={styles.spacer} />

        <div className={styles.actions}>
          <Link to="/orders"        className={styles.iconBtn} title="Orders">       <FontAwesomeIcon icon={faClipboardList} /></Link>
          <Link to="/cart"          className={styles.iconBtn} title="Cart">         <FontAwesomeIcon icon={faShoppingCart} /></Link>
          <Link to="/wishlist"      className={styles.iconBtn} title="Wishlist">     <FontAwesomeIcon icon={faHeart} /></Link>
          <Link to="/notifications" className={styles.iconBtn} title="Notifications"><FontAwesomeIcon icon={faBell} /></Link>
          <Link to="/messages"      className={styles.iconBtn} title="Messages">     <FontAwesomeIcon icon={faComments} /></Link>

          <span className={styles.greeting}>Hello, <span>Name</span></span>
          <Link to="/profile" className={styles.profileBtn} title="Profile"><FontAwesomeIcon icon={faUser} /></Link>
          <Link to="/logout"  className={styles.exitBtn}>Exit</Link>
        </div>
      </div>
    </nav>
  );
}