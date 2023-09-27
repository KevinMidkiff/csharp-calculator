/**
 * Custom exceptions to throw in the calculator application.
 */
using System;
using System.Runtime.Serialization;

namespace Calculator {
    /**
     * Syntax error exception.
     */
    [Serializable()]
    public class SyntaxError : Exception {
        public SyntaxError(string reason) : base(String.Format("Invalid Syntax: {0}", reason)) 
        {}
    }
}