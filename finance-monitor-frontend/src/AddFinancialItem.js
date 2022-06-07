import React from 'react';
class AddFinancialItem extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            occurredAt: new Date().toISOString().slice(0, 10),
            financialItemValue: 10,
            operationType: "Expenses",
            categoryIncomes: "Undefined",
            categoryExpenses: "Undefined",
            apiRootUrl: props.apiRootUrl,
            afterAddFunc: props.afterAddFunc
        };

        this.handleInputChange = this.handleInputChange.bind(this);
    }

    handleSubmit = (event) => {
        event.preventDefault();
        try {
            fetch(`${this.state.apiRootUrl}/${this.state.operationType}`, {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    value: this.state.financialItemValue,
                    category: this.state.operationType == "Expenses" ? this.state.categoryExpenses : this.state.categoryIncomes,
                    occurredAt: new Date(this.state.occurredAt).toISOString(),
                }),
            }).then(x => { this.state.afterAddFunc() });
        } catch (err) {
            console.log(err);
        }
    };


    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    Operation Type:
                </label>

                <select name="operationType" value={this.state.operationType} onChange={this.handleInputChange}>
                    <option>Expenses</option>
                    <option>Incomes</option>
                </select>
                <br />

                <label>
                    Category:
                </label>
                {this.state.operationType === "Incomes" &&
                    <select name="categoryIncomes" value={this.state.categoryIncomes} onChange={this.handleInputChange}>
                        <option>Undefined</option>
                        <option>Salary</option>
                        <option>Pension</option>
                    </select>
                }
                {this.state.operationType === "Expenses" &&
                    <select name="categoryExpenses" value={this.state.categoryExpenses} onChange={this.handleInputChange}>
                        <option>Undefined</option>
                        <option>Fuel</option>
                    </select>
                }

                <br />

                <label>
                    Financial Item Value:
                </label>
                <input
                    name="financialItemValue"
                    type="number"
                    value={this.state.financialItemValue}
                    onChange={this.handleInputChange} />

                <br />
                <label>
                    Occured At:
                </label>
                <input
                    name="occurredAt"
                    type="date"
                    value={this.state.occurredAt}
                    onChange={this.handleInputChange} />
                <br />
                <button type="submit">Add Finanacial Item</button>
            </form>
        );
    }
}

export default AddFinancialItem;