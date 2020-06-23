namespace BigSolution.Blazor
{
    /// <summary>
    /// Generate a sequence when rendering a component
    /// </summary>
    public sealed class SequenceGenerator
    {
        /// <summary>
        /// Get the next value of sequence
        /// </summary>
        /// <returns>
        /// The incremented value
        /// </returns>
        public int GetNextValue()
        {
            return _sequence++;
        }

        private int _sequence;
    }
}
