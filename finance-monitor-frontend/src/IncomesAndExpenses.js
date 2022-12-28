import React from 'react';
import dayjs from 'dayjs';
import Button from '@mui/material/Button';
import DeleteIcon from '@mui/icons-material/Delete';
import TableContainer from '@mui/material/TableContainer';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import TextField from '@mui/material/TextField';
import MenuItem from '@mui/material/MenuItem';
import Select from '@mui/material/Select';
import Typography from '@mui/material/Typography';
import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';

class IncomesAndExpenses extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
      occurredAt: dayjs(new Date()),
      financialItemValue: 10,
      operationType: "Expenses",
      categoryIncomes: "Undefined",
      categoryExpenses: "Undefined",
      apiRootUrl: props.apiRootUrl,
      afterItemChangeFunc: props.afterItemChangeFunc,
      expenseCategories: props.expenseCategories,
      incomeCategories: props.incomeCategories
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
      }).then(x => { this.state.afterItemChangeFunc() });
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
      <React.Fragment>

        <Typography component="h2" variant="h6" color="primary" gutterBottom>
           Incomes and Expenses
        </Typography>
        <form onSubmit={this.handleSubmit}>
          <TableContainer component={Paper}  >
            <Table size="small" id="incomesAndExpensesTable">
              <TableHead>
                <TableRow>
                  <TableCell>OperationType</TableCell>
                  <TableCell>Value</TableCell>
                  <TableCell>Category</TableCell>
                  <TableCell>OccurredAt</TableCell>
                  <TableCell></TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                <TableRow>
                  <TableCell>
                    <Select sx={{ minWidth: 140 }}
                    id="operationTypeToAdd"
                      name="operationType"
                      value={this.state.operationType}
                      onChange={this.handleInputChange}
                    >
                      <MenuItem value={"Expenses"}>Expenses</MenuItem>
                      <MenuItem value={"Incomes"}>Incomes</MenuItem>
                    </Select>
                  </TableCell>
                  <TableCell>
                    <TextField
                    id="financialItemValueToAdd"
                      name="financialItemValue"
                      type="number"
                      value={this.state.financialItemValue}
                      onChange={this.handleInputChange}
                    />
                  </TableCell>
                  <TableCell>                {this.state.operationType === "Incomes" &&
                    <Select  sx={{ minWidth: 140 }}
                    id="categoryIncomeToAdd"
                      name="categoryIncomes"
                      value={this.state.categoryIncomes}
                      onChange={this.handleInputChange}
                    >
                      {this.props.incomeCategories.map(item => (
                  <MenuItem value={item}>{item}</MenuItem>
                ))}
                    </Select>
                  }
                    {this.state.operationType === "Expenses" &&
                      <Select sx={{ minWidth: 140 }}
                      id="categoryExpenseToAdd"
                        name="categoryExpenses"
                        value={this.state.categoryExpenses}
                        onChange={this.handleInputChange}
                      >
                        {this.props.expenseCategories.map(item => (
                  <MenuItem value={item}>{item}</MenuItem>
                ))}
                      </Select>
                    }</TableCell>
                  <TableCell>
                    <LocalizationProvider dateAdapter={AdapterDayjs}>
                      <DesktopDatePicker
                      id="occurredAtToAdd"
                        name="occurredAt"
                        inputFormat="DD/MM/YYYY"
                        value={this.state.occurredAt}
                        onChange={(value) => this.handleInputChange({
                          target: {
                            name: "occurredAt",
                            value: value
                          }
                        })}
                        renderInput={(params) => <TextField {...params} />}
                      />
                    </LocalizationProvider>

                  </TableCell>
                  <TableCell align="right">
                    <Button id="addButton" type="submit" variant="contained">Add</Button>
                  </TableCell>
                </TableRow>

                {this.props.financialItems.map(item => (
                  <TableRow key={item.id}>
                    <TableCell>{item.operationType}</TableCell>
                    <TableCell>{item.value}</TableCell>
                    <TableCell>{item.category}</TableCell>
                    <TableCell>{dayjs(item.occurredAt).format('DD/MM/YYYY')}</TableCell>
                    <TableCell align="right">
                      <Button variant="outlined" startIcon={<DeleteIcon />} onClick={() => this.delete(item)}>
                        Delete
                      </Button>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </form>
      </React.Fragment>
    )
  }

  delete(item) {
    try {
      fetch(`${this.state.apiRootUrl}/${item.operationType}/${item.id}`, { method: "DELETE" })
        .then(x => { this.state.afterItemChangeFunc() });
    } catch (err) {
      console.log(err);
    }
  }
}

export default IncomesAndExpenses;