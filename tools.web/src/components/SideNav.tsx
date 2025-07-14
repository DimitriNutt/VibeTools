import { NavLink } from 'react-router-dom';
import './../index.css';

export default function SideNav() {
    return (
        <nav className="sidenav">
            <h2 className="sidenav-title">Vibe Tools</h2>
            <ul className="menu">
                <li><NavLink to="/tools/list" className={({ isActive }) => isActive ? "active" : ""}>List Tools</NavLink></li>
                <li><NavLink to="/tools/new" className={({ isActive }) => isActive ? "active" : ""}>Add Tool</NavLink></li>
                <li><NavLink to="/tools/search" className={({ isActive }) => isActive ? "active" : ""}>Search Tools</NavLink></li>
            </ul>
        </nav>
    );
}
