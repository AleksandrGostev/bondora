import React, {Component} from 'react';
import axios from "axios/index";

const initialState = {
    equipments: []
};

class Home extends Component {
    constructor() {
        super();
        this.state = initialState;

        this.handleRentDaysChange = this.handleRentDaysChange.bind(this);
        this.submitRequest = this.submitRequest.bind(this);
    }

    componentDidMount() {
        this.getEquipments();
    }

    resetState() {
        this.setState(initialState);
    }

    getEquipments() {
        axios.get('http://localhost:53534/api/equipments')
            .then(response => {
                this.resetState();
                this.setState({...this.state, equipments: response.data});
            });
    }

    handleRentDaysChange(index, event) {
        let equipments = this.state.equipments.slice();
        equipments[index].days = event.target.value;
        this.setState({equipments: equipments});
    }

    submitRequest() {
        // Send a POST request
        axios({
            method: 'post',
            url: 'http://localhost:53534/api/orders',
            data: {
                equipments: this.state.equipments
            }
        }).then(response => {
            this.props.history.push('/Order/'+response.data)
        });
    }

    render() {
        return (
            <div className="">
                <div className="panel panel-default">
                    <div className="panel-body">
                        <div className="container center">
                            <h3>Rental Form</h3>
                            <table className="table table-condensed table-stripped">
                                <thead>
                                <tr>
                                    <th>Equipment name</th>
                                    <th>Type</th>
                                    <th>Days to rent</th>
                                </tr>
                                </thead>
                                <tbody>
                                {
                                    this.state.equipments.map((eq, i) =>
                                        <tr key={i}>
                                            <td>{eq.title}</td>
                                            <td>{eq.type}</td>
                                            <td>
                                                <input type="number"
                                                       className="form-control"
                                                       min="0"
                                                       onChange={this.handleRentDaysChange.bind(this, i)}/>
                                            </td>
                                        </tr>)
                                }
                                </tbody>
                            </table>

                            <button className="btn btn-primary" onClick={this.submitRequest}>Rent items</button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Home;