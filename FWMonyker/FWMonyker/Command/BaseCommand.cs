/*
 ****************************************
 *                                      *
 *   Author: Kristian Dam-Jensen        *
 *                                      *
 ****************************************
 */

using System;

namespace FWMonyker.Command
{
    public abstract class BaseCommand
    {
        public event EventHandler CanExecuteChanged;
    }
}