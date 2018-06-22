import React, {Component} from 'react';
import axios from "axios/index";
import "./Order.css";
import download from 'downloadjs';

let initialState = {
    order: {},
    rentals: []
};

class Order extends Component {

    constructor(props) {
        super(props);

        this.orderId = props.match.params.orderId;
        this.state = initialState;
        this.getInvoice = this.getInvoice.bind(this);
    }

    componentDidMount() {
        this.getOrder();
    }

    resetState() {
        this.setState(initialState);
    }

    getOrder() {
        axios.get('http://localhost:53534/api/orders/' + this.orderId)
            .then(response => {
                this.resetState();
                this.setState({...this.state, order: response.data, rentals: response.data.rentals});
            });
    }

    getInvoice() {
        axios.get('http://localhost:53534/api/orders/download/' + this.orderId)
            .then(response => {
                console.log(response.data);
                download(response.data+"", "invoice-"+this.orderId, 'text/plain');
            });
    }

    render() {
        return (
            <div className="well">
                <div className="container">
                    <h2>Order nr: {this.state.order.orderId}</h2>

                    <table className="table table-condensed table-hover">
                        <thead>
                        <tr>
                            <th>Title</th>
                            <th>Type</th>
                            <th>Rental days</th>
                            <th>Price</th>
                            <th>Bonus</th>
                        </tr>
                        </thead>
                        <tbody>
                        {
                            this.state.rentals.map((rental, i) =>
                                <tr key={i}>
                                    <td>{rental.equipment.title}</td>
                                    <td>{rental.equipment.type}</td>
                                    <td>{rental.days}</td>
                                    <td>{rental.totalPrice}</td>
                                    <td>{rental.bonus}</td>
                                </tr>
                            )
                        }
                        <tr className="totals">
                            <td>Total</td>
                            <td></td>
                            <td></td>
                            <td>{this.state.order.totalPrice}</td>
                            <td>{this.state.order.totalBonus}</td>
                        </tr>
                        </tbody>
                    </table>
                    <button className="btn btn-primary" onClick={this.getInvoice}>Download invoice</button>
                </div>
            </div>
        );
    }
}

export default Order;