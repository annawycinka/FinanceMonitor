import React from 'react';
import './App.css';
import FinancialItemsTable from './FinancialItemsTable';
import AddFinancialItem from './AddFinancialItem';

class App extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      error: null,
      isLoaded: false,
      financialItems: [],
      apiRootUrl: "http://localhost:5000"
    };
  }

  refreshFinancialItems = () => {
    Promise.all([
      fetch(`${this.state.apiRootUrl}/Incomes`).then(res => res.json()),
      fetch(`${this.state.apiRootUrl}/Expenses`).then(res => res.json()),
    ]).then(([incomesJson, expensesJson]) => 
    {
      const expenses = expensesJson.map(item => (
        {
          id: item.id,
          operationType: "Expenses",
          value: item.value,
          category: item.category,
          occurredAt: item.occurredAt
        }
      ));
      const incomes = incomesJson.map(item => (
        {
          id: item.id,
          operationType: "Incomes",
          value: item.value,
          category: item.category,
          occurredAt: item.occurredAt
        }
      ));
      const financialItems = incomes.concat(expenses);
      this.setState({
        isLoaded: true,
        financialItems: financialItems
      });
    }).catch((error) => {
      this.setState({
        isLoaded: true,
        error
      });
    });
  }

  componentDidMount() {
    this.refreshFinancialItems()  
  }

  render() {
    const { error, isLoaded, financialItems } = this.state;
    if (error) {
      return <div>Błąd: {error.message}</div>;
    } else if (!isLoaded) {
      return <div>Ładowanie...</div>;
    } else {
      return (
        <div>
          <AddFinancialItem apiRootUrl={this.state.apiRootUrl} afterAddFunc={this.refreshFinancialItems}></AddFinancialItem>
          <br />
          <FinancialItemsTable 
          financialItems={financialItems}
          apiRootUrl={this.state.apiRootUrl}
          afterDeleteOrUpdateFunc={this.refreshFinancialItems}>            
          </FinancialItemsTable>
        </div>        
      );
    }
  }
}

export default App;
