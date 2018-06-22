import React, {Component} from 'react';
import axios from "axios/index";
import {Link} from 'react-router-dom';

let initialState = {
    orders: []
};

class Orders extends Component {
    constructor() {
        super();

        this.state = initialState;
    }

    resetState() {
        this.setState(initialState);
    }

    componentDidMount() {
        this.getOrders();
    }

    getOrders() {
        axios.get('http://localhost:53534/api/orders/')
            .then(response => {
                this.resetState();
                this.setState({...this.state, orders: response.data});
            });
    }

    render() {
        return (
            <div className="well">
                <div className="container">
                    <table className="table table-condensed table-hover">
                        <thead>
                        <tr>
                            <th>Id</th>
                            <td></td>
                        </tr>
                        </thead>
                        <tbody>
                        {
                            this.state.orders.map((order, i) =>
                                <tr key={i}>
                                    <td>{order.orderId}</td>
                                    <td>
                                        <Link to={'/Order/' + order.orderId}>
                                            <button className="btn btn-primary">View</button>
                                        </Link>
                                    </td>
                                </tr>
                            )
                        }
                        </tbody>
                    </table>
                </div>
            </div>
        );
    }
}

export default Orders;