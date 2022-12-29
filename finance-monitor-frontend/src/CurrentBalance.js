import * as React from 'react';
import Typography from '@mui/material/Typography';

class CurrentBalance extends React.Component {

    constructor(props) {
      super(props);  
    }

    render() {
const currentBallaneValue = this.props.financialItems.reduce(
  (accumulator, currentItem) => {
    if(currentItem.operationType === "Expenses"){
        return accumulator - currentItem.value;
    } 
    if(currentItem.operationType === "Incomes"){
        return accumulator + currentItem.value;
    } 
  }, 0);

        return (
            <React.Fragment>
              <Typography component="h2" variant="h6" color="primary" gutterBottom>
                Current Balance
              </Typography>
              <Typography component="p" variant="h2" id="currentBalance">
              {currentBallaneValue}
              </Typography>
            </React.Fragment>
          );
    }
}

export default CurrentBalance;