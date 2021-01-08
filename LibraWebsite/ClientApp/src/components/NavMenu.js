import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { useCookies, Cookies } from 'react-cookie';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
        super(props);
        const cookies = new Cookies();
        this.logout = this.logout.bind(this)
        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
          collapsed: true
        };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

    logout () {

        document.cookie = "user= ; expires = Thu, 01 Jan 1970 00:00:00 GMT"
        window.location.href = '/';
        return false;
    }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">
                <text>Libra</text>
            </NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">
                    <text>Categories</text>
                  </NavLink>
                  </NavItem>
                  <NavItem>
                      <NavLink tag={Link} className="text-dark" to="/comparison">
                             <text>Comparison</text>
                      </NavLink>
                            </NavItem>

                            {(document.cookie.indexOf('user') > -1) ?
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/" onClick={ this.logout }>
                                        <text>Logout</text>
                                    </NavLink>
                                </NavItem>
                                :
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/Login">
                                        <text>Login</text>
                                    </NavLink>
                                </NavItem>}
                               
                
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
