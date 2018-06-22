import React, {Component} from 'react';
import {Link} from 'react-router-dom'
import './Header.css';

class Header extends Component {
    constructor() {
        super();
        this.state = {
            isMenuOpen: false
        };

        this.toggleMenu = this.toggleMenu.bind(this);
    }

    toggleMenu() {
        this.setState({...this.state, isMenuOpen: !this.state.isMenuOpen});
    }

    render() {
        return (
            <div className="nav">
                <div className="nav-header">
                    <div className="nav-title">
                        <Link to={'/'}>Bondora Test</Link>
                    </div>
                </div>
                <div className="nav-btn">
                    <label htmlFor="nav-check" onClick={this.toggleMenu}>
                        <span></span>
                        <span></span>
                        <span></span>
                    </label>
                </div>
                <input type="checkbox" id="nav-check" checked={this.state.isMenuOpen}/>
                <div className="nav-links">
                    <Link to={'/'} onClick={this.toggleMenu}>Home</Link>
                    <Link to={'/Orders'} onClick={this.toggleMenu}>Orders</Link>
                </div>
            </div>
        );
    }
}

export default Header;