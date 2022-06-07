import React from 'react';
class FinancialItemsTable extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      apiRootUrl: props.apiRootUrl,
      afterDeleteOrUpdateFunc: props.afterDeleteOrUpdateFunc
    };
  }

  render() {
    return (
      <table>
        <thead>
          <tr>
            <th>OperationType</th>
            <th>Value</th>
            <th>Category</th>
            <th>OccurredAt</th>
            <th>Delete</th>
          </tr>
        </thead>
        <tbody>
          {this.props.financialItems.map(item => (
            <tr key={item.id}>
              <td>{item.operationType}</td>
              <td>{item.value}</td>
              <td>{item.category}</td>
              <td>{item.occurredAt.slice(0, 10)}</td>
              <td onClick={() => this.delete(item)}>×</td>
            </tr>))}
        </tbody>
      </table>
    )
  }

  delete(item) {
    try {
      fetch(`${this.state.apiRootUrl}/${item.operationType}/${item.id}`, { method: "DELETE" })
        .then(x => { this.state.afterDeleteOrUpdateFunc() });
    } catch (err) {
      console.log(err);
    }
  }
}

export default FinancialItemsTable;