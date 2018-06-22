import React, {Component} from 'react';
import './App.css';
import {Switch, Route} from 'react-router-dom';
import Header from './header/Header';
import Home from './home/Home';
import Orders from './orders/Orders';
import Order from './orders/Order'

class App extends Component {
    render() {
        return (
            <div>
                <Header/>

                <Switch>
                    <Route exact path='/' component={Home}/>
                    <Route exact path='/Orders' component={Orders}/>
                    <Route exact path='/Order/:orderId' component={Order}/>
                </Switch>
            </div>
        );
    }
}

export default App;
