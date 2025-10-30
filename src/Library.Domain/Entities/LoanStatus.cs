namespace Library.Domain.Entities
{
    /// <summary>
    /// Status of a book loan
    /// </summary>
    public enum LoanStatus
    {
        /// <summary>
        /// Loan is currently active
        /// </summary>
        Active,
        
        /// <summary>
        /// Book has been returned
        /// </summary>
        Returned,
        
        /// <summary>
        /// Loan is overdue
        /// </summary>
        Overdue
    }
}
